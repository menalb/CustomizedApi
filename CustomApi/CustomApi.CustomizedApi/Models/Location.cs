using CustomizedApi.Api.Models;
using System;

namespace CustomApi.CustomizedApi.Models
{
    public class Location : AbstractLocation
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", X, Y);
        }

        public override void Process(Action<CustomModel> actionToPerform)
        {
            actionToPerform(this);
        }
    }
}