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
