# The EssentialRules simple rule engine
This project contains a simple rule engine written in .net standard 2.0
It uses a very basic approach to define rules and to determine rule states.

Each rule is characarized by two funtions ``CanRun`` and ``Run`` where the first determines if the rule is ready to fire based on the facts contained in the fact base and the second actually performs the rule's intended actions.

## Facts repositories
EssentialRules hols a fact repository containing arbitrary objects. Rules can query the facts base to determine their fire readiness.
There are currently two implementations of the ``IFactRepository`` interface. A straightforward dictionary-based in-memory fact base and a timestamped fact base that allows for time-based querying.