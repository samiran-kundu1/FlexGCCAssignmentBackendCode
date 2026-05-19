import React, { useEffect, useState } from 'react'

export default function NoteList(){
  const [notes, setNotes] = useState([])
  const [title, setTitle] = useState('')
  const [content, setContent] = useState('')

  useEffect(()=>{
    fetch('/api/notes')
      .then(r=>r.json())
      .then(setNotes)
      .catch(()=>setNotes([]))
  },[])

  function create(){
    const dto = { title, content }
    fetch('/api/notes', { method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify(dto) })
      .then(r=>r.json())
      .then(n=> setNotes([n, ...notes]))
      .catch(console.error)
  }

  function remove(id){
    fetch(`/api/notes/${id}`, { method: 'DELETE' })
      .then(()=> setNotes(notes.filter(n=>n.id !== id)))
      .catch(console.error)
  }

  return (
    <div>
      <div className="controls">
        <input placeholder="Title" value={title} onChange={e=>setTitle(e.target.value)} />
        <textarea placeholder="Content" value={content} onChange={e=>setContent(e.target.value)} />
        <button onClick={create}>Create</button>
      </div>

      {notes.map(n=> (
        <div key={n.id} className="note">
          <div className="note-title">{n.title}</div>
          <div>{n.content}</div>
          <div style={{marginTop:8}}>
            <button onClick={()=>remove(n.id)}>Delete</button>
          </div>
        </div>
      ))}
    </div>
  )
}
