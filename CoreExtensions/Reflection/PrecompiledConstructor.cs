using System;
using System.Reflection;
using System.Linq.Expressions;

namespace CoreExtensions.Reflection
{
    public class PrecompiledConstructor<T>
    {
        private delegate S ConstructorActivator<out S>(params object[] args);
        private readonly ConstructorActivator<T> m_activator;

        public PrecompiledConstructor(Type type) : this(type, null)
        {
        }

        public PrecompiledConstructor(Type type, Type[]? constructorArgTypes)
        {
            if (constructorArgTypes is null || constructorArgTypes.Length == 0)
            {
                var ctor = type.GetConstructor(Type.EmptyTypes);

                if (ctor is null)
                {
                    throw new Exception($"Constructor of type {type} with argument types passed cannot be found");
                }

                this.m_activator = PrecompiledConstructor<T>.GenerateEmptyActivator(ctor);
            }
            else
            {
                var ctor = type.GetConstructor(constructorArgTypes);

                if (ctor is null)
                {
                    throw new Exception($"Constructor of type {type} with argument types passed cannot be found");
                }
                
                var @params = ctor.GetParameters();
                
                this.m_activator = PrecompiledConstructor<T>.GenerateFilledActivator(ctor, @params);
            }
        }

        public PrecompiledConstructor(ConstructorInfo? constructor)
        {
            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor), "ConstructorInfo passed cannot be null");
            }

            var @params = constructor.GetParameters();

            this.m_activator = @params is null || @params.Length == 0
                ? PrecompiledConstructor<T>.GenerateEmptyActivator(constructor)
                : PrecompiledConstructor<T>.GenerateFilledActivator(constructor, @params);
        }

        private static ConstructorActivator<T> GenerateEmptyActivator(ConstructorInfo ctor)
        {
            var expression = Expression.New(ctor);

            return Expression.Lambda<ConstructorActivator<T>>(expression).Compile();
        }

        private static ConstructorActivator<T> GenerateFilledActivator(ConstructorInfo ctor, ParameterInfo[] @params)
        {
            var parameter = Expression.Parameter(typeof(object[]), "Arguments");

            var args = new Expression[@params.Length];

            for (int i = 0; i < args.Length; ++i)
            {
                var constant = Expression.Constant(i);
                var paramType = @params[i].ParameterType;

                var paramAccessor = Expression.ArrayIndex(parameter, constant);
                var paramCast = Expression.Convert(paramAccessor, paramType);
                
                args[i] = paramCast;
            }

            var invoker = Expression.New(ctor, args);

            return Expression.Lambda<ConstructorActivator<T>>(invoker, parameter).Compile();
        }

        public T Invoke(params object[] args) => this.m_activator(args);
    }
}
