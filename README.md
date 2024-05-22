# dotNETCloudRun

CloudRunで`.NET`を動かす

```bash
export gcp_project=your-gcp-project
```

```bash
gcloud config set project $gcp_project
```

```bash
docker build -t blazorappcontainer . --no-cache --platform linux/amd64
```

```bash
gcloud artifacts repositories create blazorappcontainer --location=asia-northeast1 --repository-format=docker
```

```bash
docker tag blazorappcontainer asia-northeast1-docker.pkg.dev/dotnetlab-415211/blazorappcontainer/blazorappcontainer:latest
```

```bash
gcloud auth configure-docker asia-northeast1-docker.pkg.dev
```

```bash
docker push asia-northeast1-docker.pkg.dev/dotnetlab-415211/blazorappcontainer/blazorappcontainer:latest
```

## Port番号を変更する

```bash
export ASPNETCORE_URLS=http://+:8080
```

CloudRunにデプロイするときはデプロイ設定に上記の環境変数を入れないと動きません。

## dotnet secretsを使う

```bash
dotnet user-secrets init
dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"
```

## Googole CloudのSecret Managerを使う

```bash
dotnet add package Google.Cloud.SecretManager.V1 --version 2.5.0
```

## 環境変数を設定する

```bash
export PROJECT_ID=your-gcp-project
```

または
  
```bash
$env:PROJECT_ID="your-gcp-project"
```

## 参考

- [ASP.NET Core Blazor Server でオレオレ認証を追加したい without Cookie](https://zenn.dev/microsoft/articles/blazor-oreore-auth-part3)
- [Authentication with Google OAuth 2.0](https://blazorschool.com/tutorial/blazor-wasm/dotnet7/authentication-with-google-oauth-2-931158)
- [.NET6 Blazor Server で Google OAuth 認証を実装する](https://qiita.com/beginnnnner/items/ba54f2bc72a2584e4ae9)
- [How do I implement Blazor authentication with Google?](https://www.syncfusion.com/faq/blazor/general/how-do-i-implement-blazor-authentication-with-google)
- [Blazor Web App でOAuth認証を最小規模で使う (ASP.NET Core 8.0)](https://zenn.dev/tetr4lab/articles/1946ec08aec508)
- [ASP.NET Core Blazor の認証と承認](https://learn.microsoft.com/ja-jp/aspnet/core/blazor/security/?view=aspnetcore-8.0)
- [Add Auth0 Authentication to Blazor Web Apps](https://auth0.com/blog/auth0-authentication-blazor-web-apps/)
- [ASP.NET Core での Google 外部ログインのセットアップ](https://learn.microsoft.com/ja-jp/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-8.0)
- [ASP.NET Core での開発におけるアプリ シークレットの安全な保存](https://learn.microsoft.com/ja-jp/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#enable-secret-storage)
- [ASP.NET Core での Google 外部ログインのセットアップ](https://learn.microsoft.com/ja-jp/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-8.0)
- [Microsoft.AspNetCore.Authentication.Google](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Google#versions-body-tab)
- [ASP.NET Core Identity を使用せずにソーシャル サインイン プロバイダー認証を使用する](https://learn.microsoft.com/ja-jp/aspnet/core/security/authentication/social/social-without-identity?view=aspnetcore-8.0&viewFallbackFrom=aspnetcore-3.0)
- [Blazor template issues with Google Authentication [dotnet 8 rc2]](https://github.com/dotnet/aspnetcore/issues/51402)
