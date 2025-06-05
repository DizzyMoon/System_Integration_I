from flask import Flask, Response, stream_with_context
import time

app = Flask(__name__)

def event_stream():
    count = 0
    while True:
        yield f"data: Server time is {time.ctime()}, count = {count}\n\n"
        count += 1
        time.sleep(1)

@app.route('/stream')
def stream():
    return Response(
        stream_with_context(event_stream()),
        mimetype='text/event_stream'
    )

if __name__ == '__main__':
    app.run(debug=True, threaded=True)
