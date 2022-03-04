#!/usr/bin/env bash
# After following the process outlined in https://game.ci/docs/github/activation ...
gh secret set UNITY_LICENSE --body "$(cat ~/Downloads/Unity_v2019.2.11f1.alf)"
gh secret set UNITY_EMAIL --body "${UNITY_EMAIL}"
gh secret set UNITY_PASSWORD  --body "${UNITY_PASSWORD}"

open https://www.the-qrcode-generator.com/

