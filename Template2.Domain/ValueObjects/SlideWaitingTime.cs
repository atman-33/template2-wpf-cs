namespace Template2.Domain.ValueObjects
{
    public sealed class SlideWaitingTime : ValueObject<SlideWaitingTime>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name=""value""></param>
        public SlideWaitingTime(float value)
        {
            Value = value;
        }

        public float Value { get; }

        protected override bool EqualsCore(SlideWaitingTime other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            if (Value == null)
            {
                return 0;
            }

            return Value.GetHashCode();
        }

        public override string ToString()
        {
            if (Value == null)
            {
                return String.Empty;
            }

            return Value.ToString();
        }
    }
}
