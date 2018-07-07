namespace DeliveryService.BLL.Transformer
{
    using AutoMapper;

    /// <summary>
    /// Простой трансформер. Используется для объектов с одинаковыми полями
    /// </summary>
    public static class SimpleAutoMapperTransformer
    {
        private static readonly object Locker = new object();

        public static TResult Transform<TSource, TResult>(TSource source) where TSource : class, new() where TResult : class, new()
        {
            lock (Locker)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg => cfg.CreateMap<TSource, TResult>());
                return Mapper.Map<TSource, TResult>(source);
            }
        }
    }
}