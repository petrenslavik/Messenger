using MessengerWindowsClient.Events;
using MessengerWindowsClient.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace MessengerWindowsClient
{
    public class Router
    {
        public static Router Instance;
        public UserControl CurrentPage { get; private set; }
        public AnimationManager AnimationManager { private get; set; }
        private Dictionary<string, Type> _pages;

        public Router()
        {
            Instance = this;
            _pages = new Dictionary<string, Type>();
            RegisterPages();
        }

        protected void RegisterPages()
        {
            var asm = Assembly.GetAssembly(typeof(Router));
            var types = asm.GetExportedTypes().Where(type => type.Namespace == "MessengerWindowsClient.Pages");
            foreach (var type in types)
            {
                var attributes = type.GetCustomAttributes(typeof(PageAttribute), false);
                if (attributes.Length > 0)
                {
                    _pages.Add(((PageAttribute)attributes[0]).Name, type);
                }
            }
        }

        public void ChangePage(string newPageName, ChangePageDirection direction)
        {
            var newPage = _pages[newPageName].GetConstructor(Type.EmptyTypes).Invoke(new object[] { }) as UserControl;
            if (CurrentPage != null && AnimationManager != null)
            {
                switch (direction)
                {
                    case ChangePageDirection.Forward:
                        AnimationManager.AnimateForwardPage(newPage, CurrentPage);
                        break;
                    case ChangePageDirection.Backward:
                        AnimationManager.AnimateBackwardPage(newPage, CurrentPage);
                        break;
                }
            }

            CurrentPage = newPage;
        }
    }
}
