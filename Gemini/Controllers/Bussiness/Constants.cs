using System.Collections.Generic;

namespace Gemini.Controllers.Bussiness
{
    public class Constants
    {
        public const string CannotUpdate = "Can not update in db";
        public const string CannotDelete = "Can not delete in db";
        public const string CannotCopy = "Can not copy in db";
        public const string EntityValidationErrors = "Có 1 lỗi liên quan đến Validation hãy nhờ admin xem log để check lỗi !";
        public const string ColumnDoesNotAllow = "Có 1 trường bằng null vì vậy không thể lưu bản ghi chi tiết: ";
        public const string DuplicateKey = "Khóa chính bị lặp hãy kiểm ra lại các mã (Code) chi tiết: ";
        public const string CannotDetectError = "Không thể tìm thấy mã lỗi xem chi tiết : ";
    }

    public class DefaultImage
    {
        public const string ImageEmpty = "/Content/UserFiles/Cauhinh/ImageEmpty.jpg";
        public const string ImageEmpty109x130 = "/Content/UserFiles/Cauhinh/ImageEmpty-109x130.jpg";
        public const string ImageEmpty117x139 = "/Content/UserFiles/Cauhinh/ImageEmpty-117x139.jpg";
        public const string ImageEmpty126x143 = "/Content/UserFiles/Cauhinh/ImageEmpty-126x143.jpg";
        public const string ImageEmpty175x207 = "/Content/UserFiles/Cauhinh/ImageEmpty-175x207.jpg";
        public const string ImageEmpty200x238 = "/Content/UserFiles/Cauhinh/ImageEmpty-200x238.jpg";
        public const string ImageEmpty202x239 = "/Content/UserFiles/Cauhinh/ImageEmpty-202x239.jpg";
        public const string ImageEmpty208x238 = "/Content/UserFiles/Cauhinh/ImageEmpty-208x238.jpg";
        public const string ImageEmpty218x258 = "/Content/UserFiles/Cauhinh/ImageEmpty-218x258.jpg";
        public const string ImageEmpty290x290 = "/Content/UserFiles/Cauhinh/ImageEmpty-290x290.jpg";
        public const string ImageEmpty370x204 = "/Content/UserFiles/Cauhinh/ImageEmpty-370x204.jpg";
        public const string ImageEmpty370x237 = "/Content/UserFiles/Cauhinh/ImageEmpty-370x237.jpg";
        public const string ImageEmpty465x536 = "/Content/UserFiles/Cauhinh/ImageEmpty-465x536.jpg";
        public const string ImageEmpty569x334 = "/Content/UserFiles/Cauhinh/ImageEmpty-569x334.jpg";
        public const string ImageSeo = "/Content/UserFiles/Cauhinh/ImageSeo.jpg";
    }

    public class ConstantsImage
    {
        public const string FormatJpegImage = ".jpeg";
        public const string FormatJpgImage = ".jpg";
        public const string Slash = "%2F";
        public const string Space = "%20";
    }

    public class CrmEmailTemplate_Code
    {
        public const string ActiveAccount = "ACTIVE_ACCOUNT";
        public const string ForgotPassword = "FORGOT_PASSWORD";
        public const string AgentAdvise = "AGENT_ADVISE";
    }

    public class CrmEmailTemplate_ReplaceCode
    {
        public const string Username = "__Username__";
        public const string LinkActiveAccount = "__LinkActiveAccount__";
        public const string PasswordReseted = "__PasswordReseted__";
    }

    public class SControl_EventClick_MAIN_CONTROL
    {
        public const string btnAdd = "btnAdd";
        public const string btnEdit = "btnEdit";
        public const string btnDelete = "btnDelete";
        public const string btnCopy = "btnCopy";
        public const string btnRefresh = "btnRefresh";
        public const string btnApprove = "btnApprove";
        public const string btnReject = "btnReject";
        public const string btnRequestApprove = "btnRequestApprove";
    }

    public class BizProperty_Type
    {
        public const int Rent = 1;
        public const int Sell = 2;

        public static Dictionary<int, string> dicDesc = new Dictionary<int, string>()
        {
            {Rent,      "Cho thuê"},
            {Sell,      "Bán"},
        };
    }

    public class BizProperty_PropertyType
    {
        public const int RealEstate = 1;
        public const int Condo = 2;

        public static Dictionary<int, string> dicDesc = new Dictionary<int, string>()
        {
            {RealEstate,        "Nhà đất"},
            {Condo,             "Căn hộ chung cư"},
        };
    }

    public class BizProperty_Status
    {
        public const int InProgress = 1;
        public const int Sold = 2;

        public static Dictionary<int, string> dicDesc = new Dictionary<int, string>()
        {
            {InProgress,        "Đang rao"},
            {Sold,              "Đã bán"},
        };
    }

    public class BizPropertyAmenity_Value
    {
        public const string Spa = "Spa";
        public const string School = "Trường học";
        public const string Medic = "Y tế";

        public static Dictionary<string, string> dicDesc = new Dictionary<string, string>()
        {
            {Spa,               "Spa"},
            {School,            "Trường học"},
            {Medic,             "Y tế"},
        };
    }

    public class BizProperty_SortBy
    {
        public const int NewToOld = 1;
        public const int OldToNew = 2;
        public const int PriceLowToHigh = 3;
        public const int PriceHighToLow = 4;
        public const int MostView = 5;

        public static Dictionary<int, string> dicDesc = new Dictionary<int, string>()
        {
            {NewToOld,              "Mới nhất"},
            {OldToNew,              "Cũ nhất"},
            {PriceLowToHigh,        "Giá thấp -> cao"},
            {PriceHighToLow,        "Giá cao -> thấp"},
            {MostView,              "Xem nhiều nhất"},
        };
    }
}