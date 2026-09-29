#!/bin/sh
set -eu

envsubst '${VITE_BASE_URL}' < /etc/nginx/env.template.js > /usr/share/nginx/html/env.js
