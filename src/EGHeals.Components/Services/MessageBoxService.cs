using EGHeals.Components.Components.Shared.MessageBoxes;
using EGHeals.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace EGHeals.Components.Services
{
    public class MessageBoxService(ModalPopupService modalPopupService)
    {
        public Task<bool> ShowAsync(string title, string description, string btnText, MsgBoxType msgBoxType)
        {
            return modalPopupService.ShowDialog<bool>(builder =>
            {
                builder.OpenComponent<MessageBoxComponent>(0);

                // String parameters
                builder.AddAttribute(1, "Title", title);
                builder.AddAttribute(2, "BtnText", btnText);

                // Enum parameter
                builder.AddAttribute(3, "Type", msgBoxType);

                // On click parameter
                builder.AddAttribute(4, "OnClick", EventCallback.Factory.Create(this, () => 
                {
                    modalPopupService.Close(true);
                    //tcs.TrySetResult(true);
                }));

                // Child content
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(childBuilder =>
                {
                    childBuilder.AddContent(0, description);
                }));

                builder.CloseComponent();
            });
        }
    }
}
