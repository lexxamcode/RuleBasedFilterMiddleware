import requests
import random
import uuid

maxZoom = 6
tileServerUrl = "http://37.46.17.101:9091/TileProxy"
sessionId = uuid.uuid5(uuid.NAMESPACE_DNS, 'intruder_basic_bulk_download')
downloadFolder = "DownloadedTiles"


def basicBulkDownload():
    for x in range (0, 2**maxZoom - 1):
        for y in range (0, 2**maxZoom - 1):
            response = requests.get("{tileServerUrl}?z={z}&x={x}&y={y}&sessionId={sessionId}".format(tileServerUrl=tileServerUrl, z=maxZoom, x=x, y=y, sessionId=sessionId))
            print(maxZoom, x, y)
            print(response.status_code)

def basicBulkDownloadReverse():
    requestsCount = 0
    
    for x in range (2**maxZoom - 1, 0, -1):
        for y in range (2**maxZoom - 1, 0, -1):
            response = requests.get("{tileServerUrl}?z={z}&x={x}&y={y}&sessionId={sessionId}".format(tileServerUrl=tileServerUrl, z=maxZoom, x=x, y=y, sessionId=sessionId))
            print(maxZoom, x, y)
            print(response.status_code)
    print("I did " + str(requestsCount) + " requests!")

def randomBulkDownload(zoom: int, leftXTileIndex: int, rightXTileIndex:int, upperYTileIndex: int, downYTileIndex: int):
    alreadySent = set()

    tilesCount = 2**(2*zoom)
    while tilesCount > 0:
        x: int = random.randint(leftXTileIndex, rightXTileIndex)
        y: int = random.randint(upperYTileIndex, downYTileIndex)

        requestPayload: str = "z={z}&x={x}&y={y}&sessionId={sessionId}".format(tileServerUrl=tileServerUrl, z=zoom, x=x, y=y, sessionId=sessionId)

        if requestPayload in alreadySent:
            continue

        tilesCount -= 1
        response = requests.get("{tileServerUrl}?{requestPayload}".format(tileServerUrl=tileServerUrl, requestPayload=requestPayload))
        print(zoom, x, y, response.status_code)

if __name__ == '__main__':
    sessionId = uuid.uuid5(uuid.NAMESPACE_DNS, 'intruder_random_bulk_download_zoom_11')
    print(sessionId)
    randomBulkDownload(11, 500, 800, 20, 320)