SELECT WorkoutId, WorkoutName, VideoUrl, ThumbnailUrl FROM Workouts ORDER BY WorkoutId DESC;
UPDATE Workouts
SET ThumbnailUrl = 'https://img.youtube.com/vi/' + 
                   SUBSTRING(VideoUrl, CHARINDEX('v=', VideoUrl) + 2, 11) + 
                   '/0.jpg'
WHERE ThumbnailUrl IS NULL;