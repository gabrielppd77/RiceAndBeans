#!/bin/sh
set -eu

envsubst '${VITE_BASE_URL} ${VITE_URL_REDIRECT_REGISTER}' < /etc/nginx/env.template.js > /usr/share/nginx/html/env.js
