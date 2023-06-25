using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Gemini.Controllers.Bussiness;
using Gemini.Models._01_Hethong;
using Gemini.Models;
using Gemini.Models._02_Cms.U;

namespace Gemini.Controllers.Combo
{
    public class ComboController : GeminiController
    {
        //public JsonResult CrmEmailConfig()
        //{
        //    IEnumerable<CRM_EMAIL_CONFIG> objList = DataGemini.CRM_EMAIL_CONFIG.Where(p => p.ACTIVE).OrderBy(p => p.STT);

        //    return Json(objList.Select(c => new { MaEmailConfig = vString.GetValueTostring(c.MA_EMAIL_CONFIG), Email = c.EMAIL }), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult CrmTemplate()
        //{
        //    IEnumerable<CRM_TEMPLATE> objList = DataGemini.CRM_TEMPLATE.Where(p => p.ACTIVE).OrderBy(p => p.STT);

        //    return Json(objList.Select(c => new { MaTemplate = vString.GetValueTostring(c.MA_TEMPLATE), TenTemplate = c.TEN_TEMPLATE }), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult ErpRole()
        //{
        //    IEnumerable<ErpRole> objList = DataGemini.ErpRoles.Where(s => s.Active).OrderBy(p => p.Name);
        //    return Json(objList.Select(c => new { c.Name, c.Guid }), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult UGroup(string type)
        {
            IEnumerable<UGroup> ugroups = DataGemini.UGroups.Where(p => p.Active && p.Type == type.Trim()).OrderBy(p => p.Name);
            var data = ConvertIEnumerateUGroup(ugroups);
            var roots = BuildTreeUGroup(data);
            foreach (var item in roots)
            {
                AppendCharsUGroup(item);
            }
            return Json(data.OrderBy(x => x.RootId).Select(c => new { c.Guid, c.Name }), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<UGroupModel> ConvertIEnumerateUGroup(IEnumerable<UGroup> source)
        {
            return source.Select(item => new UGroupModel(item)).ToList();
        }

        #region UGroupTree

        public static IList<UGroupModel> BuildTreeUGroup(IEnumerable<UGroupModel> source)
        {
            IList<UGroupModel> roots = new BindingList<UGroupModel>();
            var groups = source.GroupBy(i => i.ParentGuid).ToList();
            var rootgroups = groups.FirstOrDefault(g => g.Key.HasValue == false);
            if (rootgroups != null)
            {
                roots = rootgroups.ToList();
                if (roots.Count > 0)
                {
                    var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                    int order = 0;
                    foreach (var t in roots)
                    {
                        order++;
                        t.RootId = order;
                        AddChildrenUGroup(t, dict, ref order);
                    }
                }
            }

            return roots;
        }

        private static void AddChildrenUGroup(UGroupModel node, IDictionary<Guid, List<UGroupModel>> source, ref int order)
        {
            if (source.ContainsKey(node.Guid))
            {
                node.Items = source[node.Guid];
                foreach (UGroupModel t in node.Items)
                {
                    order++;
                    t.RootId = order;
                    AddChildrenUGroup(t, source, ref order);
                }
            }
            else
            {
                node.Items = new List<UGroupModel>();
            }
        }

        private static void AppendCharsUGroup(UGroupModel uGroup, string append = "")
        {
            if (uGroup.Items != null && uGroup.Items.Any())
            {
                append += ">> ";
                foreach (var item in uGroup.Items)
                {
                    item.Name = string.Format("{0} {1}", append, item.Name);
                    AppendCharsUGroup(item, append);
                }
            }
        }

        #endregion

        public JsonResult CrmEmailTemplate()
        {
            IEnumerable<CrmEmailTemplate> objList = DataGemini.CrmEmailTemplates.Where(p => p.Active).OrderBy(p => p.SubjectEmail);
            return Json(objList.Select(c => new { c.Guid, c.SubjectEmail }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrmEmailSetting()
        {
            IEnumerable<CrmEmailSetting> objList = DataGemini.CrmEmailSettings.Where(p => p.Active).OrderBy(p => p.Email);
            return Json(objList.Select(c => new { c.Guid, c.Email }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SMenu(string type)
        {
            IEnumerable<SMenu> sMenus;
            if (string.IsNullOrEmpty(type))
            {
                sMenus = DataGemini.SMenus.OrderBy(p => p.Name);
            }
            else
            {
                sMenus = DataGemini.SMenus.Where(p => p.Type == type).OrderBy(p => p.Name);
            }
            var data = ConvertIEnumerateSMenu(sMenus);
            var roots = BuildTreeSMenu(data);
            foreach (var item in roots)
            {
                AppendCharsSMenu(item);
            }
            return Json(data.OrderBy(p => p.RootId).Select(c => new { c.Guid, c.Name }), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SMenuModel> ConvertIEnumerateSMenu(IEnumerable<SMenu> source)
        {
            return source.Select(item => new SMenuModel(item)).ToList();
        }

        #region SMenuTree

        public static IList<SMenuModel> BuildTreeSMenu(IEnumerable<SMenuModel> source)
        {
            IList<SMenuModel> roots = new BindingList<SMenuModel>();
            var groups = source.GroupBy(i => i.GuidParent).ToList();
            var rootgroups = groups.FirstOrDefault(g => g.Key.HasValue == false);
            if (rootgroups != null)
            {
                roots = rootgroups.ToList();
                if (roots.Count > 0)
                {
                    var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                    int order = 0;
                    foreach (var t in roots)
                    {
                        order++;
                        t.RootId = order;
                        AddChildrenSMenu(t, dict, ref order);
                    }
                }
            }

            return roots;
        }

        private static void AddChildrenSMenu(SMenuModel node, IDictionary<Guid, List<SMenuModel>> source, ref int order)
        {
            if (source.ContainsKey(node.Guid))
            {
                node.Items = source[node.Guid];
                foreach (SMenuModel t in node.Items)
                {
                    order++;
                    t.RootId = order;
                    AddChildrenSMenu(t, source, ref order);
                }
            }
            else
            {
                node.Items = new List<SMenuModel>();
            }
        }

        private static void AppendCharsSMenu(SMenuModel sMenu, string append = "")
        {
            if (sMenu.Items != null && sMenu.Items.Any())
            {
                append += ">> ";
                foreach (var item in sMenu.Items)
                {
                    item.Name = string.Format("{0} {1}", append, item.Name);
                    AppendCharsSMenu(item, append);
                }
            }
        }

        #endregion

        public JsonResult SRole()
        {
            IEnumerable<SRole> sMenus = DataGemini.SRoles.Where(p => p.Active).OrderBy(p => p.Name);
            return Json(sMenus.Select(c => new { c.Guid, c.Name }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BizPropertyType()
        {
            return Json(BizProperty_Type.dicDesc.Select(c => new { c.Key, c.Value }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BizPropertyPropertyType()
        {
            return Json(BizProperty_PropertyType.dicDesc.Select(c => new { c.Key, c.Value }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BizPropertyStatus()
        {
            return Json(BizProperty_Status.dicDesc.Select(c => new { c.Key, c.Value }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AmenityValues()
        {
            return Json(BizPropertyAmenity_Value.dicDesc.Select(c => new { c.Key, c.Value }), JsonRequestBehavior.AllowGet);
        }
    }
}