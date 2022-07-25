import os
import moviepy.video.io.ImageSequenceClip

def sort_by_number(text):
    
    f = int(text.split("_")[0])
    s = int(text.split("x")[1].split("-")[0])
    
    return (f,s)


image_folder = "\\"
video_name = 'video.avi'

images = [img for img in os.listdir(image_folder,) if img.endswith(".png")]


images.sort(
    key = sort_by_number
)

clip = moviepy.video.io.ImageSequenceClip.ImageSequenceClip(images, fps=45)
clip.write_videofile('my_video.mp4')

