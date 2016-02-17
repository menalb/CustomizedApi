using CustomApi.CustomizedApi.Validators;
using CustomizedApi.Api.Models;
using FluentValidation.Attributes;
using Messages;
using System;

namespace CustomApi.CustomizedApi.Models
{
    [Validator(typeof(ProductModelValidator))]
    public class CustomProductModel : DynamicProductModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }

        public override ILoadProductMessage GetMessageToSend()
        {
            return new LoadProductMessage { Name = Name, Brand = Brand };
        }

        public override void Process(Action<CustomModel> actionToPerform)
        {
            actionToPerform(this);
        }
    }

    public class LoadProductMessage : ILoadProductMessage
    {
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}