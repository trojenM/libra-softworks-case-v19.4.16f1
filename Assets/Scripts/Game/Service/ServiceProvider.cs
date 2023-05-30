using System;
using System.Collections.Generic;
using Game.Core.Grid;
using Game.Core.ItemBase;
using Game.UI;

namespace Game.Service
{
    public static class ServiceProvider
    {
        private static readonly Dictionary<Type, IProvidable> RegisterDictionary = new Dictionary<Type, IProvidable>();

        public static T GetManager<T>() where T : class, IProvidable
        {
            if (RegisterDictionary.ContainsKey(typeof(T)))
            {
                return (T) RegisterDictionary[typeof(T)];
            }

            return null;
        }

        public static T Register<T>(T target) where T : class, IProvidable
        {
            RegisterDictionary.Add(typeof(T), target);
            return target;
        }

        public static ItemFactory GetItemFactory
        {
            get { return GetManager<ItemFactory>(); }
        }
    
        public static InterfaceManager GetInterfaceManager
        {
            get { return GetManager<InterfaceManager>(); }
        }

        public static GridCellPool GetGridCellPool
        {
            get { return GetManager<GridCellPool>(); }
        }
    }
}
