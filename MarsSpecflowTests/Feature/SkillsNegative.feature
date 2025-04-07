@skills @negative
Feature: Negative scenarios for adding skills

  As a user
  I want to be prevented from adding invalid skill entries
  So that my profile contains only valid information

  @negative
  Scenario Outline: [Negative] Attempt to add skill with invalid or duplicate input
    Given User is on the Profile Page
    When User navigates to skills tab
    And User attempts to add skill "<skill>" with level "<level>"
Then The correct skill error message "<errorType>" should be displayed



    Examples:
      | skill              | level       | errorType            |
      |                    | Expert      | BlankSkill           |
      | Java               |             | BlankSkillLevel      |
      | Automation Testing | Beginner    | DuplicateSkill       |
      | Python             | Invalid     | InvalidSkillLevel    |
