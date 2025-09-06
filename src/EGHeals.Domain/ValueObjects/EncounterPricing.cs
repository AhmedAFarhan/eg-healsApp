﻿namespace EGHeals.Domain.ValueObjects
{
    public record EncounterPricing
    {
        public ReferralDoctorCost ReferralDoctorCost { get; private set; } = default!;
        public RadiologistCost RadiologistCost { get; private set; } = default!;
        public TechnicianCost TechnicianCost { get; private set; } = default!;
        public bool IsReferralDoctorCostPercentage { get; private set; } = default!;
        public bool IsRadiologistCostPercentage { get; private set; } = default!;
        public bool IsTechnicianCostPercentage { get; private set; } = default!;
        public decimal Cost { get; } = default!;
        public decimal Discount { get; } = default!;
        public bool IsDiscountPercentage { get; } = default!;
        public decimal Net
        {
            get
            {
                decimal net = IsDiscountPercentage ? Cost - ((Discount / 100) * Cost) : Cost - Discount;

                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(net);

                return net;
            }
        }
        public decimal NetAfterDeduction
        {
            get
            {
                decimal referralDoctorCost = IsReferralDoctorCostPercentage ? Cost - ((ReferralDoctorCost.Value / 100) * Cost) : ReferralDoctorCost.Value;
                decimal radiologistCost = IsRadiologistCostPercentage ? Cost - ((RadiologistCost.Value / 100) * Cost) : RadiologistCost.Value;
                decimal technicianCost = IsTechnicianCostPercentage ? Cost - ((TechnicianCost.Value / 100) * Cost) : TechnicianCost.Value;
                decimal net = Net - referralDoctorCost - radiologistCost - technicianCost;

                return net;
            }
        }

        protected EncounterPricing() { }

        private EncounterPricing(ReferralDoctorCost referralDoctorCost, RadiologistCost radiologistCost, TechnicianCost technicianCost, bool isReferralDoctorCostPercentage, bool isRadiologistCostPercentage, bool isTechnicianCostPercentage, decimal cost, decimal discount, bool isDiscountPercentage)
        {
            ReferralDoctorCost = referralDoctorCost;
            RadiologistCost = radiologistCost;
            TechnicianCost = technicianCost;
            IsReferralDoctorCostPercentage = isReferralDoctorCostPercentage;
            IsRadiologistCostPercentage = isRadiologistCostPercentage;
            IsTechnicianCostPercentage = isTechnicianCostPercentage;
            Cost = cost;
            Discount = discount;
            IsDiscountPercentage = isDiscountPercentage;
        }

        public static EncounterPricing Of(ReferralDoctorCost referralDoctorCost, RadiologistCost radiologistCost, TechnicianCost technicianCost, bool isReferralDoctorCostPercentage, bool isRadiologistCostPercentage, bool isTechnicianCostPercentage, decimal cost, decimal discount, bool isDiscountPercentage)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(cost, 0);
            ArgumentOutOfRangeException.ThrowIfLessThan(discount, 0);

            return new EncounterPricing(referralDoctorCost, radiologistCost, technicianCost, isReferralDoctorCostPercentage, isRadiologistCostPercentage, isTechnicianCostPercentage, cost, discount, isDiscountPercentage);
        }
    }
}
