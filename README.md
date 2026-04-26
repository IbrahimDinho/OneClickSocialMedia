# OneClickSocialMedia

[![Deploy](https://github.com/IbrahimDinho/OneClickSocialMedia/actions/workflows/socialmedia-deploy.yml/badge.svg)](https://github.com/IbrahimDinho/OneClickSocialMedia/actions/workflows/socialmedia-deploy.yml)
![.NET](https://img.shields.io/badge/.NET-10.0-purple)

OneClickSocialMedia is a responsive web application that lets you post to multiple social media platforms from one place, designed to work on both desktop and mobile devices.


Currently supported / planned platforms:
- X (Twitter)
- Facebook
- Instagram


## Live Site
Site - [oneclicksocialmedia-geh7baazekeuh7f3.westeurope-01.azurewebsites.net](https://oneclicksocialmedia-geh7baazekeuh7f3.westeurope-01.azurewebsites.net)
Note: This application is hosted on Azure using a free/basic tier. Due to cold start, the site may take a few minutes to load after inactivity.If you encounter an error on first visit, please wait a moment and refresh the page.

Email associated with site - noreply.oneclicksocialmedia@gmail.com

## Tech Stack

- **Backend:** ASP.NET Core MVC (.NET)
- **Data:** Entity Framework Core + Azure SQL Database
- **Hosting:** Azure App Service (Web App)
- **CI/CD:** GitHub Actions (deploy pipeline)

---

## Features

- Post content to multiple platforms from a single UI
- Platform settings page for storing API credentials safely (per user)
- Single Sign-On (SSO) using providers (Google, Facebook, X/Twitter)

---

## Setup & Documentation

- Create developer apps on X/Facebook/Instagram and linking them to the accounts from where the posts will occur on
- Generating API keys/tokens with the correct permissions Read/Write
- If prompted during setup you can use the oneclicksocialmedia website as the redirect url (X/Twitter may make you do this)
- For Twitter when creating an app you will need to add credits/payment to be able to post as unfortunetly its now pay per use, read more here -> https://devcommunity.x.com/t/announcing-the-launch-of-x-api-pay-per-use-pricing/256476

## Security Notes

- Authentication is required to access the application. Users must be logged in to manage social media settings. Currently only login/register/forgot-password pages are publicly accessible.
- User authentication is handled using **ASP.NET Core Identity**. A zero-trust policy is applied across every page unless explicitly marked as publicly accessible (login/register/forgot password). 
- User passwords are **securely hashed**.
- Password reset functionality is implemented using ASP.NET Core Identity secure token generation.
- Password recovery links are delivered via email and contain a secure, time-limited, user-bound token, ensuring that only the intended recipient can reset the account password.
- Two-Factor Authentication (2FA) via email is supported and can be enabled by users for enhanced account security. When enabled, users must provide a time-sensitive one-time verification code sent to their registered email address during login.
- 2FA codes are generated using ASP.NET Core Identity token providers and are user-bound, secure, and time-limited.
- API tokens and secrets are **encrypted before being stored** in the database.
- Secrets are **never returned in plain text** to the UI after saving. Instead, a masked placeholder (e.g. `********`) is shown.
- Database access is restricted to the application via Azure SQL configuration.
- Users can authenticate using either email & password or Single Sign-On (SSO) via Google, Facebook, or X (Twitter)

