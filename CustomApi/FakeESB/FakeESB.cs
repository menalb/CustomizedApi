using Messages;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FakeESB
{
    public interface IFakeESB
    {
        void Send(IMessage message);
    }

    public class FakeESB : IFakeESB
    {
        public void Send(IMessage message)
        {
            Type myType = message.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            Console.WriteLine($"Message sent (type: {myType.ToString()})");
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(message, null);
                Console.WriteLine($"    {prop.Name} -> {propValue.ToString()}");
            }
        }
    }
}