Feature: QCRulesManagement

Scenario: Add new Standard Deviation Rule
	Given a logged in user
	When user creates an standard deviation rule with the values
	| Name       | WithinControl | Comment                      | NumberOfControls | StandardDeviationLimits |
	| "Rule1446" | True          | "An Standard deviation rule" | 2                | 2                       |
	Then the rule is saved on the system with the values
	| Name       | WithinControl | Comment                      | NumberOfControls | StandardDeviationLimits |
	| "Rule1446" | True          | "An Standard deviation rule" | 2                | 2                       |
	
Scenario: Edit existing Standard Deviation Rule
	Given a logged in user
	And a Standard Deviation rule existing in the system with the values
	| Name       | WithinControl | Comment                      | NumberOfControls | StandardDeviationLimits |
	| "Rule1612" | False          | "An Standard deviation rule 1612" | 2                | 6                       |	
	When user modifies the values
	| Name       | Comment | NumberOfControls | StandardDeviationLimits |
	| "Rule1612" |"An Standard deviation rule 1615" | 5                | 2                       |
	Then the rule is saved on the system with the values
	| Name       | WithinControl | Comment                      | NumberOfControls | StandardDeviationLimits |
	| "Rule1612" | False          | "An Standard deviation rule 1615"| 5                | 2                       |	
	
Scenario: Deactivate an existing rule
	Given a logged in user
	And a Standard Deviation rule existing in the system with the values
	| Name       | WithinControl | Comment                      | NumberOfControls | StandardDeviationLimits |
	| Rule2215   | True          | An Standard deviation rule 2215 | 2                | 9                       |	
	When user deactivates the rule "Rule2215"
	Then the rule "Rule2215" is deactivated

Scenario: Reactivate a deactivated rule
	Given a logged in user
	And a Standard Deviation rule existing in the system with the values
	| Name       | WithinControl | Comment                      | NumberOfControls | StandardDeviationLimits |
	| Rule1623  | False | An Standard deviation rule 1623 | 3                | 5                       |	
	And the "Rule1623" is deactivated
	When user reactivates the rule "Rule1623"
	Then the rule "Rule1623" is activated

Scenario: Assign tests
	Given a logged in user
	And a Standard Deviation rule existing in the system with the values
         | Name | WithinControl | Comment | NumberOfControls | StandardDeviationLimits |
         | 2238     | true              | comment        | 3                 | 1                        |
	And an existing ApplicationTest with test code "22393"	
	When the ApplicationTest "22393" is assigned to the qc rule "2238"
	Then the ApplicationTest "22393" and the qc rule "2238" are linked

Scenario: Unassign tests
	Given a logged in user
	And a Standard Deviation rule existing in the system with the values
         | Name | WithinControl | Comment | NumberOfControls | StandardDeviationLimits |
         | 1731     | false              | comment 1731        | 1                 | 2                        |
	And an existing ApplicationTest with test code "16201"
	And the ApplicationTest "16201" is assigned to the qc rule "1731"
	When the ApplicationTest "16201" is unassigned to the qc rule "1731"
	Then the ApplicationTest "16201" and the qc rule "1731" are not linked

Scenario: Validate QC results
	Given an existing ApplicationTest with test code "7260"
	And an existing control for the test "7260" with the values
		| StandardDeviation | TargetValue |
		| 0.5      | 0.9         |
	And a Standard Deviation rule existing in the system with the values
         | Name | WithinControl | Comment | NumberOfControls | StandardDeviationLimits |
         | Rule 606     | True               | Comment 201        | 1                 | 2                        |
	And the ApplicationTest "7260" is assigned to the qc rule "Rule 606"
	When a qc result with for the test "7260" with value 0.9 is received
	Then the qc result is succesfully evaluated

