using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Web.Trees;

namespace UBookable.TreeEvents
{
    public class DuplicateContent : ApplicationEventHandler
    {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            TreeControllerBase.MenuRendering += ContentTreeController_MenuRendering;
        }


        private void ContentTreeController_MenuRendering(Umbraco.Web.Trees.TreeControllerBase sender, Umbraco.Web.Trees.MenuRenderingEventArgs e)
        {
            switch (sender.TreeAlias)
            {
                case "content":
                    
                    //creates a menu action that will open /umbraco/currentSection/itemAlias.html;
                    var i = new Umbraco.Web.Models.Trees.MenuItem("duplicateNode", "Duplicate");

                    //optional, if you dont want to follow conventions, but do want to use a angular view
                    i.AdditionalData.Add("actionView", "/App_Plugins/DuplicateNode/DuplicateNodeView.html");

                    //sets the icon to icon-wine-glass 
                    i.Icon = "user-females-alt";

                    //insert at index 5
                    if (e.Menu.Items.Count > 5) {
                        e.Menu.Items.Insert(5, i);
                    }
                    
                    break;
            }
        }
    }
}