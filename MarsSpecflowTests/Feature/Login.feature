Feature: LoginFeature

A short summary of the feature


@tag1
Scenario Outline: Verify login with different valid credentials
Given User is on the login page and registered
When User enters <username> and <password>
Then User should be navigated to the Home Page

Examples:
  | username           | password |
  | anya@gmail.com     | 123123   |
  | tanya@gmail.com    | tanya1   |