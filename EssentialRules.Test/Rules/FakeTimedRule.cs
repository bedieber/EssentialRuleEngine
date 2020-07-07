using System;
using System.Collections.Generic;
using System.Linq;

namespace EssentialRules.Test.Rules
{
    public class FakeTimedRule : TimedBaseRule
    {
        public FakeTimedRule(TimedFactRepository repository) : base(repository)
        {
        }

        public override bool CanRun()
        {
            return false;
        }

        public override bool Run()
        {
            return true;
        }
    }
}