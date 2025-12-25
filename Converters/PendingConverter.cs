using System.Globalization;
using ITFundManager.Models;

namespace ITFundManager.Converters;

public class PendingConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is ProposalStatus status)
        {
            return status == ProposalStatus.Pending;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
