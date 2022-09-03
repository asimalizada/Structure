using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Helpers
{
    public static class BusinessRuleHelper
    {
        public static IResult Check(params IResult[] rules)
        {
            foreach (var rule in rules)
            {
                if (!rule.Success)
                {
                    return rule;
                }
            }

            return null;
        }
    }
}
