ROOT:=$(dir $(realpath $(firstword $(MAKEFILE_LIST))))

RED="\033[1;31m"
GREEN="\033[1;32m"
YELLOW="\033[1;33m"
BLUE="\033[1;34m"
NOCOLOR="\033[0m\033[K"

COMPOSE=docker-compose -p __DT_PROJECT_NAME_devcontainer -f ${ROOT}.devcontainer/docker-compose.yml
LOGTAIL=--tail 50


.PHONY: help
.DEFAULT_GOAL := help

MANAGE=python manage.py

##
## Main targets
##
help: ## display enhanced help message
	@grep -E '(^[a-zA-Z_-]+:.*?##.*$$)|(^##)' $(MAKEFILE_LIST) | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[32m%-15s\033[0m %s\n", $$1, $$2}' | sed -e 's/\[32m##/[33m/'

run-server: ## run django server
	${MANAGE} runserver

requirements: ## install requirements from requirements.txt
	pip install -r requirements.txt

##
## DB targets
##

showmigrations: ## SHOW MIGRATIONS
	${MANAGE} showmigrations

migrate: ## APPLY MIGRATIONS
	${MANAGE} migrate

dbshell: ## DB SHELL
	${MANAGE} dbshell

createsuperuser: ## CREATE SUPER USER
	${MANAGE} createsuperuser

makemigrations: ## MAKE MIGRATIONS
	${MANAGE} makemigrations

##
## Docker
##
ps: ## start mysql db
	${COMPOSE} ps
