using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace BookingManagementSystem.Helpers;
public class CoordinateConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double coordinate)
        {
            // Kiểm tra xem có phải vĩ độ hay kinh độ (parameter là "latitude" hoặc "longitude")
            var isLatitude = parameter != null && parameter.ToString() == "latitude";
            return ConvertToDms(coordinate, isLatitude);
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        // Không cần thiết, vì chỉ chuyển đổi một chiều từ Decimal sang DMS
        throw new NotImplementedException();
    }

    private string ConvertToDms(double degree, bool isLatitude)
    {
        int d = (int)degree;  // Độ
        int m = (int)((Math.Abs(degree) - Math.Abs(d)) * 60);  // Phút
        double s = (Math.Abs(degree) - Math.Abs(d) - m / 60.0) * 3600;  // Giây

        // Làm tròn giây đến 1 chữ số thập phân
        s = Math.Round(s, 1);

        // Kiểm tra lại giây nếu quá 60
        if (s >= 60)
        {
            m += (int)(s / 60);  // Thêm vào phút
            s = s % 60;          // Cập nhật lại giây
        }

        string direction = "";
        if (isLatitude)
        {
            direction = degree >= 0 ? "N" : "S";  // Vĩ độ: Bắc (N) hoặc Nam (S)
        }
        else
        {
            direction = degree >= 0 ? "E" : "W";  // Kinh độ: Đông (E) hoặc Tây (W)
        }

        // Trả về kết quả dưới định dạng DMS
        return $"{Math.Abs(d)}°{m}'{s}\"{direction}";
    }
}
