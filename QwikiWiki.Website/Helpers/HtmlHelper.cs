using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using QwikiWiki.Common.Models;


namespace QwikiWiki.Helpers
{
    public static class HtmlHelperExtensions
    {
        //public static IHtmlString CreateImageTags(this HtmlHelper html, string publisherCode)
        //{
        //    IHtmlString result = MvcHtmlString.Empty;

        //    ICardLogic cardLogic = BusinessLogicProvider.CardLogic();
        //    List<ImageViewModel> imageViewModelList = new List<ImageViewModel>();
        //    imageViewModelList = cardLogic.GetImageCollection(publisherCode);
        //    StringBuilder sb = new StringBuilder();

        //    foreach (ImageViewModel imgViewModel in imageViewModelList)
        //    {
        //        //sb.AppendFormat(string.Format("<img id=\"{0}\" src=\"{0}\" Alt=\"{1}\" Class=\"{2}\" />", imgVM.Src, imgVM.Alt, imgVM.CssClass));

        //        TagBuilder builder = new TagBuilder("img");

        //        builder.MergeAttribute("src", imgViewModel.Src);
        //        builder.MergeAttribute("alt", imgViewModel.Alt);
        //        builder.MergeAttribute("class", imgViewModel.CssClass);
        //        sb.Append(builder.ToString(TagRenderMode.SelfClosing));

        //        sb.AppendFormat("\n");
        //    }
        //    result = MvcHtmlString.Create(sb.ToString());

        //    return result;
        //}  


        public static IHtmlString CreateSpellLayout(this HtmlHelper html, SpellModel spell)
        {
            const string cssClass = "emphaisedData";

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"spellContent\">");
            sb.Append("<div class=\"spellDetails\">");
            sb.AppendFormat("Name: <span class=\"{1}\">{0}</span><br/>", spell.Name, cssClass);
            if(!string.IsNullOrEmpty(spell.Cost) && spell.Cost != "0 NoCost")
                sb.AppendFormat("Cost:  <span class=\"{1}\">{0}</span><br/>", spell.Cost, cssClass);
            if (!string.IsNullOrEmpty(spell.Cooldown))
                sb.AppendFormat("Cooldown: <span class=\"{1}\">{0}</span><br/>", spell.Cooldown, cssClass);
            if (!string.IsNullOrEmpty(spell.Range))
                sb.AppendFormat("Range:  <span class=\"{1}\">{0}</span><br/>", spell.Range, cssClass);
            sb.Append("</div>");

            if (!string.IsNullOrEmpty(spell.Description))
                sb.AppendFormat("<div class=\"spellDescription\">{0}</div>", spell.Description);

            
            //foreach (string stat in spell.Stats)
            //{
            //    sb.AppendLine(stat + "<br/>");                
            //}
            sb.Append("</div>");
            IHtmlString result = MvcHtmlString.Create(sb.ToString());
            return result;
        }   
    }
}