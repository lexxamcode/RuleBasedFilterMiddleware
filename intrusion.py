import requests

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

if __name__ == '__main__':
    basicBulkDownloadReverse()