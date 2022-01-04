select * from Post

INSERT INTO Comment (PostId, UserProfileId, Subject, Content, CreateDateTime)
OUTPUT INSERTED.ID
VALUES (2, 1, 'Test', 'Test', '2008-12-31')

select * from Comment