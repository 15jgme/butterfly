import os, subprocess, time, ffmpeg
from mastodon import Mastodon
from apscheduler.schedulers.background import BackgroundScheduler


scheduler = BackgroundScheduler()

#   Set up Mastodon
mastodon = Mastodon(
    access_token = os.environ['BUTTERFLYTOKEN'],
    api_base_url = 'https://botsin.space/'
)

render_name = "output.avi"
output_name = "output.webm"

def render():
    """
    Renders the scene to output.avi and converts it using ffmpeg to a webm
    """
    global render_name
    global output_name

    godot_path = os.environ['GODOTPATH'] # Fetch the path to the godot executable
    repo_path = os.environ['BUTTERFLYPATH'] # Get the root directory of the repo
    command = f"{godot_path} --path {repo_path}/render/ --write-movie {render_name} --resolution 1980x1200 --quit-after 10000"

    print(f"Render command to run: {command}")
    subprocess.call(command, shell=True) # Run the render command

    # Converson from avi to webm
    input_file = f"{repo_path}/render/{render_name}"
    output_file = f"{repo_path}/clips/{output_name}"

    print("Running conversion")
    stream = ffmpeg.input(input_file)
    stream = ffmpeg.output(stream, output_file)
    stream = ffmpeg.overwrite_output(stream)
    ffmpeg.run(stream) # run the ffmpeg conversion

def publish():
    """
    Publishes the clip
    """
    global output_name
    repo_path = os.environ['BUTTERFLYPATH'] # Get the root directory of the repo
    media = mastodon.media_post(f"{repo_path}/clips/{output_name}", description="Today's random Lorenz attractor") # Create the media
    print(media)
    time.sleep(1000)
    mastodon.status_post("Today's random Lorenz attractor", media_ids=media, in_reply_to_id=None, sensitive=False, spoiler_text=None)

def render_and_pub():
    """
    Runs the complete, render and publish loop
    """
    render()
    publish()

scheduler.add_job(func=render_and_pub, trigger='interval', hours=24)