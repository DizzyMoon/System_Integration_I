import asyncio
import websockets

async def echo (websocket):
    async for message in websocket:
        print (f"Received: {message}")
        await websocket.send(f"Echo: {message}")

async def main():
    async with websockets.serve(echo, "localhost", 8765):
        print("Server started on ws://localhost:8765")
        await asyncio.Future()

if __name__ == "__main__":
    asyncio.run(main())
