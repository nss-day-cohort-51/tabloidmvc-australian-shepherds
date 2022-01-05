ALTER TABLE Post 
DROP CONSTRAINT FK_Post_Category;

ALTER TABLE Post
ALTER COLUMN CategoryId int;

ALTER TABLE Post
ADD CONSTRAINT FK_Post_Category foreign key (CategoryId) 
REFERENCES Category(Id)
ON DELETE set NULL;
