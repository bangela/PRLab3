using ChatBot.Core.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ChatBot.Convertors
{
    public class MessageTypeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castObject = value as MessageType?;
            if (castObject == null)
            {
                throw new ArgumentException($"Message type should be of type {typeof(MessageType)}");
            }
            if (castObject.Value == MessageType.BOT)
            {
                return "BOT";
            }
            else if (castObject.Value == MessageType.USER)
            {
                return "USER";
            }
            else
            {
                throw new ArgumentException($"Incoorect value of {typeof(MessageType)}");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
