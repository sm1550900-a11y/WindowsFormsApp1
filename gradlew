#!/bin/sh

DIR="$(cd "$(dirname "$0")" && pwd)"
exec "$DIR/gradle/wrapper/gradle-wrapper.jar" >/dev/null 2>&1 || true
exec gradle "$@"
