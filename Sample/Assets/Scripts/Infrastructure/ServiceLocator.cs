using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class ServiceLocator
    {
        private static ServiceLocator m_serviceLocator;
        
        private Dictionary<Type, object> m_services = new();

        public static void Register<T>(T instance)
            where T : class
        {
            m_serviceLocator ??= new ServiceLocator();
            m_serviceLocator.m_services.Add(typeof(T), instance);
        }

        public static T Resolve<T>()
            where T : class
        {
            if (m_serviceLocator == null)
            {
                throw new NullReferenceException("ServiceLocator is null");
            }

            return m_serviceLocator.m_services[typeof(T)] as T;
        }

        public static void Clear()
        {
            m_serviceLocator?.m_services.Clear();
        }
    }
}