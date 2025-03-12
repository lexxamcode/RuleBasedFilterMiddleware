import requests
import random

maxZoom = 6
tileServerUrl = "http://localhost:5279/tiles"
downloadFolder = "DownloadedTiles"


def basicBulkDownload():
    for x in range (0, 2**maxZoom - 1):
        for y in range (0, 2**maxZoom - 1):
            response = requests.get("{tileServerUrl}?z={z}&x={x}&y={y}".format(tileServerUrl=tileServerUrl, z=maxZoom, x=x, y=y))
            print(maxZoom, x, y)
            print(response.status_code)

def basicBulkDownloadReverse():
    requestsCount = 0
    
    for x in range (2**maxZoom - 1, 0, -1):
        for y in range (2**maxZoom - 1, 0, -1):
            response = requests.get("{tileServerUrl}?z={z}&x={x}&y={y}".format(tileServerUrl=tileServerUrl, z=maxZoom, x=x, y=y))
            print(maxZoom, x, y)
            print(response.status_code)
    print("I did " + str(requestsCount) + " requests!")

def randomBulkDownload(zoom: int, leftXTileIndex: int, rightXTileIndex:int, upperYTileIndex: int, downYTileIndex: int):
    alreadySent = set()

    tilesCount = 2**(2*zoom)
    while tilesCount > 0:
        x: int = random.randint(leftXTileIndex, rightXTileIndex)
        y: int = random.randint(upperYTileIndex, downYTileIndex)

        requestPayload: str = "z={z}&x={x}&y={y}".format(tileServerUrl=tileServerUrl, z=zoom, x=x, y=y)

        if requestPayload in alreadySent:
            continue

        tilesCount -= 1
        response = requests.get("{tileServerUrl}?{requestPayload}".format(tileServerUrl=tileServerUrl, requestPayload=requestPayload))
        print(zoom, x, y, response.status_code)

if __name__ == '__main__':
    randomBulkDownload(6, -100, 100, 0, 100)