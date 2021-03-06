﻿//// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
//// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
//// Author:					Joe Audette
//// Created:					2015-05-18
//// Last Modified:			2015-06-10
//// 

//using Microsoft.AspNet.Mvc.Core;
//using Microsoft.AspNet.Mvc.Rendering;

//namespace cloudscribe.Core.Web.Helpers
//{
//    public static class HtmlHelpers
//    {
//        /// <summary>
//        /// trying to solve the issue here
//        /// http://stackoverflow.com/questions/24757849/checkbox-inputs-badly-aligned-when-theres-no-accompanying-label-using-bootstrap
//        /// 
//        /// so basically this helper should render the checkbox inside the label element
//        /// then the hidden field needed for mvc model binding after that outside the label
//        /// in order to keep the html valid (2 inputs inside a label is not valid)
//        /// </summary>
//        /// <param name="htmlHelper"></param>
//        /// <param name="name"></param>
//        /// <param name="isChecked"></param>
//        /// <param name="htmlAttributes"></param>
//        /// <returns></returns>
//        public static HtmlString CheckBoxForBootstrap(this HtmlHelper htmlHelper,
//            string name,
//            bool isChecked)
//        {

//            TagBuilder checkbox = new TagBuilder("input");
//            checkbox.Attributes.Add("type", "checkbox");
//            checkbox.Attributes.Add("name", name);
//            checkbox.Attributes.Add("id", name);
//            checkbox.Attributes.Add("data-val", "true");
//            checkbox.Attributes.Add("value", "true");

//            if (isChecked)
//                checkbox.Attributes.Add("checked", "checked");

//            TagBuilder label = new TagBuilder("label");
//            //nest the checkbox inside the label
//            label.InnerHtml = checkbox.ToString(TagRenderMode.Normal);

//            // to understand why mvc needs this hidden field see this:
//            //http://stackoverflow.com/questions/2697299/asp-net-mvc-why-is-html-checkbox-generating-an-additional-hidden-input
//            TagBuilder hidden = new TagBuilder("input");
//            hidden.Attributes.Add("type", "hidden");
//            hidden.Attributes.Add("name", name);
//            hidden.Attributes.Add("value", "false");

//            HtmlString htmlString = new HtmlString(
//                label.ToString(TagRenderMode.Normal)
//                + hidden.ToString(TagRenderMode.Normal)
//                );

//            return htmlString;
//        }


//    }
//}
