window.mapInterop = {
    maps: {},
    geocoder: null,

    initializeWithGeolocation: function (id, dotnetHelper, zoom) {
        if (!navigator.geolocation) {
            console.warn("Geolocation is not supported by this browser.");
            // fallback: Cairo
            this.initialize(id, dotnetHelper, {
                center: { lat: 30.0444, lng: 31.2357 },
                zoom: zoom
            });
            return;
        }
        navigator.geolocation.getCurrentPosition(
            (pos) => {
                const userLocation = {
                    lat: pos.coords.latitude,
                    lng: pos.coords.longitude
                };

                this.init(id, {
                    center: userLocation,
                    zoom: zoom
                }, dotnetHelper);
            },
            (err) => {
                console.error("Geolocation failed:", err);
                // fallback: Cairo
                this.initialize(id, dotnetHelper, {
                    center: { lat: 30.0444, lng: 31.2357 },
                    zoom: zoom
                });
            }
        );
    },

    init: function (id, options, dotnetHelper) {
        //Make shure the map id is exist
        const element = document.getElementById(id);
        if (!element) {
            console.error("Map container not found:", id);
            return;
        }

        //Create new map object
        const map = new google.maps.Map(document.getElementById(id), options);
        this.maps[id] = map;

        // create geocoder if not already
        if (!this.geocoder) {
            this.geocoder = new google.maps.Geocoder();
        }

        // Notify Blazor when dragging starts
        map.addListener("dragstart", () => {
            dotnetHelper.invokeMethodAsync("OnMapMoveStarted");
        });

        // Trigger event when map stops moving
        map.addListener("idle", () => {
            const center = map.getCenter();
            if (center && this.geocoder) {
                this.geocoder.geocode({ location: center.toJSON() }, (results, status) => {
                    if (status === "OK" && results[0]) {
                        const parts = window.mapInterop.extractAddressParts(results[0]);
                        const compact = [parts.street, parts.area, parts.district, parts.state, parts.city].filter(Boolean).join(", ");
                        dotnetHelper.invokeMethodAsync("OnAddressChangedHandler", compact, center.lat(), center.lng());
                    }
                });
            }
        });
    },

    extractAddressParts: function (result) {
        const components = result.address_components;
        const get = (type) =>
            components.find(c => c.types.includes(type))?.long_name || "";

        return {
            street: `${get("street_number")} ${get("route")}`.trim(),
            area: get("neighborhood") || get("sublocality") || get("locality"),
            city: get("locality"),
            district: get("administrative_area_level_2"),
            state: get("administrative_area_level_1"),
            country: get("country"),
        };
    },

    setCenter: function (id, lat, lng) {
        if (this.maps[id]) {
            this.maps[id].setCenter({ lat, lng });
        }
    },

    setZoom: function (id, zoom) {
        if (this.maps[id]) {
            this.maps[id].setZoom(zoom);
        }
    },

    addMarker: function (id, markerOptions) {
        if (this.maps[id]) {
            return new google.maps.Marker({
                ...markerOptions,
                map: this.maps[id]
            });
        }
    },

    dispose: function (id) {
        if (this.maps[id]) {
            google.maps.event.clearInstanceListeners(this.maps[id]);
            delete this.maps[id];
        }
    }
};