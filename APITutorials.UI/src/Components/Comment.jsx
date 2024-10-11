import { useEffect, useState } from "react";
import '../Components/Comment.css';

function Comment() {
    const [comments, setComments] = useState([]);

    useEffect(() => {
        fetch(`http://localhost:5143/api/comment`)
            .then(res => res.json())
            .then(data => {
                setComments(data);
            })
            .catch(error => console.error('Error fetching comments:', error));
    }, []);

    return (
        <div className="App">
            <table className="styled-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Title</th>
                        <th>Content</th>
                        <th>Created On</th>
                        <th>Created By</th>
                        <th>Stock ID</th>
                    </tr>
                </thead>
                <tbody>
                    {comments.map(comment => (
                        <tr key={comment.id}>
                            <td>{comment.id}</td>
                            <td>{comment.title}</td>
                            <td>{comment.content}</td>
                            <td>{comment.createdOn}</td>
                            <td>{comment.createdBy}</td>
                            <td>{comment.stockId}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Comment;
