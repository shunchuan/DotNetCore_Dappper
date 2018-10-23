using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DotNetCore_Dappper.Infrastructure.Ioc
{
    public class AutofacContainer
    {
        /// <summary>
        /// Autofac 容器
        /// </summary>
        protected IContainer Container;

        /// <summary>
        /// 容器生成器
        /// </summary>
        protected ContainerBuilder Builder;

        /// <summary>
        /// 初始化一个<see cref="AutofacIocContainer"/>类型的实例
        /// </summary>
        public AutofacContainer() { }

        /// <summary>
        /// 初始化一个<see cref="AutofacIocContainer"/>类型的实例
        /// </summary>
        /// <param name="builder">容器生成器</param>
        public AutofacContainer(ContainerBuilder builder)
        {
            Builder = builder;
        }

        /// <summary>
        /// 设置容器
        /// </summary>
        /// <param name="container">容器</param>
        public void SetContainer(object container)
        {
            Container = (IContainer)container;
        }

        /// <summary>
        /// 获取容器
        /// </summary>
        /// <typeparam name="T">容器类型</typeparam>
        /// <returns></returns>
        public T GetContainer<T>()
        {
            return (T)Container;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public T Create<T>(string name = null)
        {
            return (T)Create(typeof(T), name);
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public object Create(Type type, string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Container.Resolve(type);
            }

            return Container.ResolveNamed(name, type);
        }

        /// <summary>
        /// 创建集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public List<T> CreateList<T>(string name = null)
        {
            var result = CreateList(typeof(T), name);
            if (result == null)
            {
                return new List<T>();
            }

            return ((IEnumerable<T>)result).ToList();
        }

        /// <summary>
        /// 创建集合
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public object CreateList(Type type, string name = null)
        {
            Type serviceType = typeof(IEnumerable<>).MakeGenericType(type);
            return Create(serviceType, name);
        }

        /// <summary>
        /// 该类型是否已注册
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public bool IsRegistered(Type type, string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Container.IsRegistered(type);
            }

            return Container.IsRegisteredWithName(name, type);
        }

        /// <summary>
        /// 该类型是否已注册
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public bool IsRegistered<T>(string name = null)
        {
            return IsRegistered(typeof(T), name);
        }

        public void Build()
        {
            Container = Builder.Build();
        }
    }
}
