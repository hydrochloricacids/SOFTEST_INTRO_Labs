@Factorial
Feature: UsingCalculatorFactorial
  In order to count arrangements quickly
  As a calculator user
  I want to be told the factorial of a whole number

  Scenario: Factorial of a normal number
    Given I have a calculator
    When I have entered 5 into the calculator and press factorial
    Then the factorial result should be 120

  Scenario: Factorial of zero
    Given I have a calculator
    When I have entered 0 into the calculator and press factorial
    Then the factorial result should be 1

  Scenario Outline: Reject an unsupported factorial value
    Given I have a calculator
    When I have entered <value> into the calculator and press factorial
    Then the factorial should be rejected

    Examples:
      | value |
      | -1    |
      | 21    |