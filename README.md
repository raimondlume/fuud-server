# fuud? - a .NET Core 2.2 REST API server

A RESTful API for consumption by [fuud-client](https://github.com/raimondlume/fuud-client). See a live demo of the web-app at [fuud.xyz](https://fuud.xyz).

All the API endpoints can be found at [fuud.raimondlu.me/swagger](https://fuud.raimondlu.me/swagger)



## Crawlers

Food data (as of now):

- Daily lunch restaurants (ITC and U06), data from [daily.ee/ee/lunch-offers/](https://www.daily.ee/ee/lunch-offers/)
- BitStop (IT College, Raja 4c), data from a private *Google Sheets* file 



## Deployment

The server is deployed as a docker container running on a Ubuntu VPS.

**CD/CI** pipeline:

1. push to master branch
2. push triggers a new build at Docker Hub (private)
3. webhook to a Ubuntu VPS on build completion
4. pull new image and run container
