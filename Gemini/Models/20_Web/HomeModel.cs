using Gemini.Models._01_Hethong;
using Gemini.Models._03_Biz;
using System.Collections.Generic;

namespace Gemini.Models._20_Web
{
    public class HomeModel
    {
        public List<BizPropertyModel> NewestProperties { get; set; }

        public List<BizPropertyModel> MostViewProperties { get; set; }
    }

    public class CategoryModel
    {
        public List<BizPropertyModel> Properties { get; set; }
    }

    public class SearchModel
    {
        public List<BizPropertyModel> Properties { get; set; }
    }

    public class PropertyModel
    {
        public BizPropertyModel Property { get; set; }

        public SUserModel User { get; set; }

        public PropertySendEmailModel SendEmailModel { get; set; }
    }

    public class PropertySendEmailModel
    {
        public string To { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }
    }
}