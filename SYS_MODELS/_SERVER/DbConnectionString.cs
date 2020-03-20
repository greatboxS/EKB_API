using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS_MODELS
{
    public class DbConnectionString
    {
        public static string _SYS_HIS { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_SYS_HIS;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _SCHEDULE { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_SCHEDULE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _SEQUENCE { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_SEQUENCE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _COMPONENT { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_COMPONENT;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _BUILDING { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_BUILDING;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _EMPLOYEE { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_EMPLOYEE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _STOCK_MANAGEMENT { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_STOCK_MANAGEMENT;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _EKANBAN_TASK { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_EKANBAN_TASK;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _BEAMCUT_HIS { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_BEAMCUT_HIS;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public static string _BEAMCUT_TASK { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_BEAMCUT_TASK;User ID=sa;Password=shc@1234;Connect Timeout=30;";
    }

    public interface IDbName
    {
        string _SYS_HIS { get; set; } 
        string _SCHEDULE { get; set; }
        string _SEQUENCE { get; set; }
        string _COMPONENT { get; set; }
        string _BUILDING { get; set; }
        string _EMPLOYEE { get; set; }
        string _STOCK_MANAGEMENT { get; set; }
        string _EKANBAN_TASK { get; set; } 
        string _BEAMCUT_HIS { get; set; }
        string _BEAMCUT_TASK { get; set; }
    }

    public class FakeDb : IDbName
    {
        public string _SYS_HIS { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_SYS_HIS;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _SCHEDULE { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_SCHEDULE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _SEQUENCE { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_SEQUENCE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _COMPONENT { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_COMPONENT;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _BUILDING { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_BUILDING;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _EMPLOYEE { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_EMPLOYEE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _STOCK_MANAGEMENT { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_STOCK_MANAGEMENT;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _EKANBAN_TASK { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_EKANBAN_TASK;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _BEAMCUT_HIS { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_BEAMCUT_HIS;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _BEAMCUT_TASK { get; set; } = @"Data Source=DESKTOP-7N0K68V\SQLEXPRESS;Initial Catalog=_BEAMCUT_TASK;User ID=sa;Password=shc@1234;Connect Timeout=30;";
    }

    public class RealDb : IDbName
    {
        public string _SYS_HIS { get; set; } = @"Server=10.4.2.23;Initial Catalog=_SYS_HIS;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _SCHEDULE { get; set; } = @"Server=10.4.2.23;Initial Catalog=_SCHEDULE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _SEQUENCE { get; set; } = @"Server=10.4.2.23;Initial Catalog=_SEQUENCE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _COMPONENT { get; set; } = @"Server=10.4.2.23;Initial Catalog=_COMPONENT;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _BUILDING { get; set; } = @"Server=10.4.2.23;Initial Catalog=_BUILDING;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _EMPLOYEE { get; set; } = @"Server=10.4.2.23;Initial Catalog=_EMPLOYEE;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _STOCK_MANAGEMENT { get; set; } = @"Server=10.4.2.23;Initial Catalog=_STOCK_MANAGEMENT;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _EKANBAN_TASK { get; set; } = @"Server=10.4.2.23;Initial Catalog=_EKANBAN_TASK;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _BEAMCUT_HIS { get; set; } = @"Server=10.4.2.23;Initial Catalog=_BEAMCUT_HIS;User ID=sa;Password=shc@1234;Connect Timeout=30;";
        public string _BEAMCUT_TASK { get; set; } = @"Server=10.4.2.23;Initial Catalog=_BEAMCUT_TASK;User ID=sa;Password=shc@1234;Connect Timeout=30;";
    }
}
