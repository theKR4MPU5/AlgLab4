using System;

namespace AlgSort.task_2
{
    delegate bool ConditionDelegate(IComparable value);

    class Condition
    {
        private readonly int _attributeNum;
        private readonly ConditionDelegate _function;

        public Condition(TableWorker table, string attribute, ConditionDelegate conditionFunction)
        {
            _function = conditionFunction;
            _attributeNum = Array.IndexOf(table.Attributes, attribute);
        }

        public bool Satisfies(Element[] elements)
        {
            return _function((IComparable) elements[_attributeNum].Value);
        }
    }
}