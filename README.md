# dotNETCloudRun

CloudRunで`.NET`を動かす

```bash
gcloud builds submit --config=cloudbuild.yaml --substitutions="_LOCATION=asia-northeast1,_REPOSITORY=blazorappcontainer,_IMAGE=gcr.io/$gcp_project/blazorappcontainer,_SERVICE_NAME=blazorappcontainer" .
```
