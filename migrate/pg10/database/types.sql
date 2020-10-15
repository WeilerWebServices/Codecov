create type service as enum ('github', 'bitbucket', 'gitlab', 'github_enterprise', 'gitlab_enterprise', 'bitbucket_server');

create type plans as enum('5m', '5y', '25m', '25y', '50m', '50y', '100m', '100y', '250m', '250y', '500m', '500y', '1000m', '1000y', '1m', '1y',
                          'v4-10m', 'v4-10y', 'v4-20m', 'v4-20y', 'v4-50m', 'v4-50y', 'v4-125m', 'v4-125y', 'v4-300m', 'v4-300y',
                          'users');

create type sessiontype as enum('api', 'login');

create type languages as enum('javascript', 'shell', 'python', 'ruby', 'perl', 'dart', 'java', 'c', 'clojure', 'd', 'fortran', 'go', 'groovy', 'kotlin', 'php', 'r', 'scala', 'swift', 'objective-c', 'xtend');

create type pull_state as enum('open', 'closed', 'merged');

create type commit_state as enum('pending', 'complete', 'error', 'skipped');

create type plan_providers as enum('github');
