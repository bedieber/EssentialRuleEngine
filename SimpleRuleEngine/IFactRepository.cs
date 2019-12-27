using System;
using System.Collections.Generic;

namespace SimpleRuleEngine
{
    public interface IFactRepository
    {
        IEnumerable<T> FindAll<T>(params Predicate<T>[] func);
    }
}