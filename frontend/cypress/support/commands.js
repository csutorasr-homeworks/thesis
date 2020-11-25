// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add("login", (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add("drag", { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add("dismiss", { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite("visit", (originalFn, url, options) => { ... })

Cypress.Commands.add('login', (email, password) => {
  cy.visit('/');
  cy.get('input').type('test@test.com{enter}');
  cy.url().should('not.eq', Cypress.config().baseUrl + '/account/login');
  cy.get('input')
    .eq(0)
    .type(email || 'csutorasr@gmail.com');
  cy.get('input')
    .eq(1)
    .type(password || '!23QWEasd{enter}');
  cy.url().should('eq', Cypress.config().baseUrl + '/');
});
