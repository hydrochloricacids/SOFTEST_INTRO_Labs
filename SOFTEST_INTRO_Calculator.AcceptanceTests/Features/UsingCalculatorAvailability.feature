@Availability
Feature: UsingCalculatorAvailability
  In order to calculate MTBF and Availability
  As someone who struggles with maths
  I want to be able to use my calculator to do this

  Scenario: Calculating MTBF
    Given I have a calculator
    When I have entered 1000 and 4 into the calculator and press MTBF
    Then the result should be 250

  Scenario: Calculating Availability
    Given I have a calculator
    When I have entered 200 and 50 into the calculator and press Availability
    Then the result should be 0.8

  Scenario: Reject MTBF when there are no failures
    Given I have a calculator
    When I have entered 1000 and 0 into the calculator and press MTBF
    Then the reliability calculation should be rejected

  Scenario: Reject Availability with a negative repair time
    Given I have a calculator
    When I have entered 200 and -1 into the calculator and press Availability
    Then the reliability calculation should be rejected

  Scenario: Calculating Availability from named reliability values
    Given I have a calculator
    And the reliability values are
      | MTBF | MTTR |
      | 90   | 10   |
    When I calculate Availability from these values
    Then the result should be 0.9