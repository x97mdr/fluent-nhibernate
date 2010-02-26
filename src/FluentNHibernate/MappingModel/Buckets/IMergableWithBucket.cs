namespace FluentNHibernate.MappingModel.Buckets
{
    public interface IMergableWithBucket
    {
        void MergeWithBucket(IMemberBucketInspector bucket);
    }
}