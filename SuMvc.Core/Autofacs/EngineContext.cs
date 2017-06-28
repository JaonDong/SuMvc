using System;

namespace SuMvc.Core.Autofacs
{
    public class EngineContext
    {
        private static readonly Lazy<IEngine> Lazy = new Lazy<IEngine>(() => new AutofacEngine());

        public static IEngine Current => Lazy.Value;
    }
}